using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class Cell : MonoBehaviour
{
    private Button _button;
    public Piece PositionedPiece { get; private set; }
    public int Row { get; private set; }
    public int Column { get; private set; }
    public bool IsOccupied { get; private set; }
    private Image _cellImage;
    private Sequence _sequence;
    private bool _isSequencePlaying;

    private void Start()
    {
        _button = GetComponent<Button>();
        _cellImage = GetComponent<Image>();
        _button.onClick.AddListener(OnCellPress);
        EventManager.OnStopCellFeedback += StopSequence;
    }

    private void OnDestroy()
    { 
        EventManager.OnStopCellFeedback -= StopSequence;
    }

    public void Initialize(int row, int column)
    {
        Row = row;
        Column = column;
    }
    
    private void OnCellPress()
    {
        EventManager.TriggerOnCellPress(this);
    }

    public void SetPiece(Piece piece)
    {
        IsOccupied = piece;
        PositionedPiece = piece;
    }
    
    public void StartAvailableFeedback()
    {
        if (_isSequencePlaying) return;
        _sequence = AnimationHelper.FadeSequence(_cellImage, 0.75f, LoopType.Yoyo, Ease.Linear, Ease.Linear);
        _isSequencePlaying = true;
    }

    public void StopSequence()
    {
        AnimationHelper.FadeImage(_cellImage, 1, 0.75f);
        _sequence.Kill(true);
        _isSequencePlaying = false;
    }
}
