﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using MattEland.FSharpGeneticAlgorithm.Genetics;

namespace MattEland.FSharpGeneticAlgorithm.WindowsClient.ViewModels
{
    public class SimulationResultViewModel : NotifyPropertyChangedBase
    {
        private readonly Genes.SimulationResult _result;
        private readonly ObservableCollection<GameStateViewModel> _states;
        private int _currentIndex;

        public SimulationResultViewModel(Genes.SimulationResult result)
        {
            _result = result;
            _states = new ObservableCollection<GameStateViewModel>();
            foreach (var state in _result.states)
            {
                _states.Add(new GameStateViewModel(state));
            }
            Brain = new BrainInfoViewModel(result.brain);
        }

        public double Score => _result.score;
        public string ScoreText => $"Score: {Score:F1}";
        public int Id => Brain.Id;

        public string DisplayText => $"Brain {Id} - {ScoreText}";

        public IEnumerable<GameStateViewModel> States => _states; 

        public GameStateViewModel SelectedState => _states[CurrentIndex];

        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                if (value == _currentIndex) return;
                _currentIndex = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedState));
            }
        }

        public int MaxStateIndex => _states.Count - 1;
        public BrainInfoViewModel Brain { get; }
        public Genes.SimulationResult Model => _result;
    }
}