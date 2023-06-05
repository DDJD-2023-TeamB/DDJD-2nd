using UnityEngine;
using System.Collections;

public class AbsorbingState : GenericState
{
    private Player _context;
    private ElementSource _catchedElementSource;
    private Coroutine _absorbCoroutine;

    public AbsorbingState(StateContext context, GenericState superState)
        : base(context, superState)
    {
        _context = (Player)context;
    }

    public override void Enter()
    {
        base.Enter();
        _context.Animator.SetBool("IsAbsorbing", true);
        _context.StartCoroutine(InitiateAbsorb());
    }

    public override void Exit()
    {
        base.Exit();
        _context.Animator.SetBool("IsAbsorbing", false);
        _catchedElementSource?.StopTransfer(_context);
        if (_absorbCoroutine != null)
        {
            _context.StopCoroutine(_absorbCoroutine);
        }
    }

    public override void StateUpdate() { }

    private IEnumerator InitiateAbsorb()
    {
        yield return new WaitForSeconds(1f);

        //RAycast to find the closest element source
        //If the closest element source is within range, absorb it
        //If not, do nothing

        RaycastHit hit;
        //Draw ray
        Debug.DrawRay(
            _context.RightHand.transform.position,
            _context.transform.forward * 2f,
            Color.red,
            1f
        );
        if (
            Physics.Raycast(
                _context.RightHand.transform.position,
                _context.transform.forward,
                out hit,
                2f
            )
        )
        {
            ElementSource elementSource = hit.collider.GetComponent<ElementSource>();
            if (elementSource != null)
            {
                elementSource.Transfer(_context, _context.RightHand.transform);
                _catchedElementSource = elementSource;
                _absorbCoroutine = _context.StartCoroutine(AbsorbCoroutine());
            }
        }
    }

    private IEnumerator AbsorbCoroutine()
    {
        Element element = _catchedElementSource.Element;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            _context.Status.RestoreMana(element, 5);
        }
    }
}
