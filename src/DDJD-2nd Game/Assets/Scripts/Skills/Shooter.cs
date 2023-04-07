using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private GameObject _leftSpell;
    private GameObject _rightSpell;

    public GameObject LeftSpell{get{return _leftSpell;} set{_leftSpell = value;}}
    public GameObject RightSpell{get{return _rightSpell;} set{_rightSpell = value;}}



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject CreateLeftSpell(Skill skill, Vector3 position){
        _leftSpell = GameObject.Instantiate(skill.SpellPrefab, position, Quaternion.identity);
        SkillComponent skillComponent = _leftSpell.GetComponent<SkillComponent>();
        skillComponent.SetCaster(gameObject);
        skillComponent.SetSkill(skill);
        return _leftSpell;
    }

    public GameObject CreateRightSpell(Skill skill, Vector3 position){
        _rightSpell = GameObject.Instantiate(skill.SpellPrefab, position, Quaternion.identity);
        SkillComponent skillComponent = _rightSpell.GetComponent<SkillComponent>();
        skillComponent.SetCaster(gameObject);
        skillComponent.SetSkill(skill);
        return _rightSpell;
    }

    public void CancelLeftShoot(){
        if(_leftSpell != null){
            EndShoot(_leftSpell);
            _leftSpell = null;
        }
    }

    public void CancelRightShoot(){
        if(_rightSpell != null){
            EndShoot(_rightSpell);
            _rightSpell = null;
        }
    }

    public void CancelShots(){
        CancelLeftShoot();
        CancelRightShoot();
    }


    public void Shoot(GameObject spell, Vector3 direction){
        SkillComponent skillComponent = spell.GetComponent<SkillComponent>();
        skillComponent.Shoot(direction);
        if(_leftSpell == spell){
            _leftSpell = null;
        }
        else if(_rightSpell == spell){
            _rightSpell = null;
        }
        
    }

    private void EndShoot(GameObject spell){
        Destroy(spell);
    }
}
