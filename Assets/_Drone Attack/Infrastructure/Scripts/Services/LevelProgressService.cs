﻿namespace _Drone_Attack.Infrastructure.Scripts.Services
{
    public class LevelProgressService : ILevelProgressService
    {
        public LevelProgressWatcher LevelProgressWatcher { get; set; }
        
        public void InitForLevel(LevelProgressWatcher levelController) => 
            LevelProgressWatcher = levelController;
    }
}