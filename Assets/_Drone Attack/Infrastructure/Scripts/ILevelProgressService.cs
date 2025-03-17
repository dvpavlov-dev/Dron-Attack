namespace _Drone_Attack.Infrastructure.Scripts
{
    public interface ILevelProgressService
    {
        LevelProgressWatcher LevelProgressWatcher { get; set; }
        
        void InitForLevel(LevelProgressWatcher levelController);
    }
}