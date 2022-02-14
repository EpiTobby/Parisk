namespace Parisk
{
    public enum ActionCost
    {
        DestroyBuilding = 30,
        ExecutePrisoners = 30,
        DebateMin = 5,
        DebateMax = 25,
        PressureOnElected = 15,
        CreateNewsPaperControl = 2,
        CreateNewsPaperInertia = 5,
        SendScout = 5,
        AttackMin = 10,
        AttackMax = 20,
        AttackFailRate = 30,
        GermanPact = 20,
        NationalGuardReinstatement = 20,
        RigElectionSuccessRate = 15,
        RigElectionSuccess = 30,
        RigElectionFailure = 15,
    }

    public enum EventCost
    {
        DestroyBuildingOnEvent = 10,
        InstallCouncilCityHall = 5,
        RestrainPressFreedom = 3,
    }
}