public class BossPhase
{
    public int _phaseIndex;
    public int _maxRuneCount;
    public int _runeVariety;

    public bool _hasTornado;

    private float _healthToAdvance;

    public BossPhase(
        int phaseIndex,
        int maxRuneCount,
        int runeVariety,
        bool hasTornado,
        float healthToAdvance
    )
    {
        _phaseIndex = phaseIndex;
        _maxRuneCount = maxRuneCount;
        _runeVariety = runeVariety;
        _hasTornado = hasTornado;
        _healthToAdvance = healthToAdvance;
    }

    public int PhaseIndex
    {
        get { return _phaseIndex; }
    }

    public int MaxRuneCount
    {
        get { return _maxRuneCount; }
    }

    public int RuneVariety
    {
        get { return _runeVariety; }
    }

    public bool HasTornado
    {
        get { return _hasTornado; }
    }

    public float HealthToAdvance
    {
        get { return _healthToAdvance; }
    }
}
