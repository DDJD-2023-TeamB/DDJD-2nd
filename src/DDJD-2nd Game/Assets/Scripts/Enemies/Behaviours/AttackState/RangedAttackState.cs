using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RangedAttackState : EnemyAttackState
{
    private Coroutine _attackCoroutine;
    private new RangedEnemy _context;
    private int _attackTries = 0;
    private int _maxAttackTries = 3;

    private int _maxAttacksInRow;
    private int _currentAttackInRow = 0;

    private int _currentShots = 0;

    private bool _stopAimingAtExit;

    private Coroutine _sendPlayerSightCoroutine;

    public RangedAttackState(RangedEnemy enemy)
        : base(enemy)
    {
        _context = enemy;
    }

    protected override void Attack() { }

    protected override void Move() { }

    public override void Enter()
    {
        base.Enter();
        _context.Animator.SetBool("IsAiming", true);
        _context.AimComponent.StartAim();
        _attackCoroutine = _context.StartCoroutine(AttackCoroutine());
        _context.Shooter.OnShoot += OnShoot;
        _attackTries = 0;
        _currentAttackInRow = 0;
        _currentShots = 0;
        _maxAttacksInRow = Random.Range(
            _context.EnemySkills.MinAttacksInRow,
            _context.EnemySkills.MaxAttacksInRow
        );
        _stopAimingAtExit = true;

        _sendPlayerSightCoroutine = _context.StartCoroutine(SendPlayerSightMessage());
    }

    public override void Exit()
    {
        base.Exit();

        _context.Animator.SetBool("IsAiming", !_stopAimingAtExit);
        if (_stopAimingAtExit)
        {
            _context.AimComponent.StopAim();
        }
        if (_attackCoroutine != null)
        {
            _context.StopCoroutine(_attackCoroutine);
        }

        _context.Shooter.CancelShots();
        _context.Shooter.OnShoot -= OnShoot;

        if (_sendPlayerSightCoroutine != null)
        {
            _context.StopCoroutine(_sendPlayerSightCoroutine);
        }
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        FacePlayer();
    }

    private IEnumerator AttackCoroutine()
    {
        //Wait one frame
        yield return 0;
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(_context.EnemySkills.AttackSpeed);
        }
    }

    private void Shoot()
    {
        AimedSkill aimedSkill;
        Transform origin;
        bool isLeft;
        bool canAttack = _context.Shooter.ChooseSpellToUse(out aimedSkill, out origin, out isLeft);
        if (canAttack)
        {
            if (_currentAttackInRow >= _maxAttacksInRow)
            {
                return;
            }
            _context.Shooter.Shoot(aimedSkill, origin, isLeft);
            _attackTries = 0;
            _currentAttackInRow++;
        }
        else
        {
            _attackTries++;
            if (_attackTries >= _maxAttackTries)
            {
                _context.ChangeState(new RangedRepositionState(_context));
            }
        }
    }

    private void FacePlayer()
    {
        Vector3 playerPosition = _context.Player.transform.position;
        playerPosition.y = _context.transform.position.y;
        Quaternion rotation = Quaternion.LookRotation(playerPosition - _context.transform.position);
        _context.transform.rotation = Quaternion.Slerp(
            _context.transform.rotation,
            rotation,
            Time.deltaTime * 7.5f
        );
    }

    private void OnShoot()
    {
        _currentShots++;
        if (_currentShots >= _maxAttacksInRow)
        {
            //End 'combo'
            _currentShots = 0;
            _currentAttackInRow = 0;
            _maxAttacksInRow = Random.Range(
                _context.EnemySkills.MinAttacksInRow,
                _context.EnemySkills.MaxAttacksInRow
            );
            int random = Random.Range(0, 100);
            if (random > 75)
            {
                _context.ChangeState(new RangedRepositionState(_context, Random.Range(2.5f, 4.5f)));
            }
            else
            {
                _stopAimingAtExit = false;
                _context.ChangeState(
                    new EnemyWaitState(
                        _context,
                        Random.Range(
                            _context.EnemySkills.MinAttackComboCooldown,
                            _context.EnemySkills.MaxAttackComboCooldown
                        )
                    )
                );
            }
        }
    }

    private IEnumerator SendPlayerSightMessage()
    {
        while (true)
        {
            _context.EnemyCommunicator.SendMessageToEnemies(
                new PlayerSightedMessage(_context.Player.transform.position)
            );
            yield return new WaitForSeconds(2.0f);
        }
    }
}
