using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CalculateScore : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _centerHitsText;
    [SerializeField] private TMP_Text _normalHitsText;
    [SerializeField] private TMP_Text _numberOfBulletsText;
    [SerializeField] private RegularPoints _regularPoints;
    [SerializeField] private RegularPoints _regularPoints2;
    [SerializeField] private ResetTargets _resetTargets;
    [SerializeField] private FireBullet _fireBullet;

    private void Update()
    {
        if (_resetTargets.isResetClicked)
        {
            _regularPoints.score = 0;
            _regularPoints.normalHits = 0;
            _regularPoints.centerHits = 0;
            _regularPoints2.score = 0;
            _regularPoints2.normalHits = 0;
            _regularPoints2.centerHits = 0;
            //_fireBullet.numberOfBullets = 9;
            //_fireBullet.needToReload = false;
            _resetTargets.isResetClicked = false;
        }
        else
        {
            _scoreText.SetText((_regularPoints.score + _regularPoints2.score).ToString());
            _centerHitsText.SetText((_regularPoints.centerHits + _regularPoints2.centerHits).ToString());
            _normalHitsText.SetText((_regularPoints.normalHits + _regularPoints2.normalHits).ToString());
            _numberOfBulletsText.SetText(_fireBullet.numberOfBullets.ToString());
        }
        
    }
}
