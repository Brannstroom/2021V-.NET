﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Ex1WTS.Core.Models;
using Ex1WTS.Core.Services;

using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Uwp.UI.Controls;

namespace Ex1WTS.ViewModels
{
    public class ListDetailsViewModel : ObservableObject
    {
        private SampleOrder _selected;

        public SampleOrder Selected
        {
            get { return _selected; }
            set { SetProperty(ref _selected, value); }
        }

        public ObservableCollection<SampleOrder> SampleItems { get; private set; } = new ObservableCollection<SampleOrder>();

        public ListDetailsViewModel()
        {
        }

        public async Task LoadDataAsync(ListDetailsViewState viewState)
        {
            SampleItems.Clear();

            var data = await SampleDataService.GetListDetailsDataAsync();

            foreach (var item in data)
            {
                SampleItems.Add(item);
            }

            if (viewState == ListDetailsViewState.Both)
            {
                Selected = SampleItems.First();
            }
        }
    }
}
