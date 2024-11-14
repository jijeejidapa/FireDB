using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FireDB.Models;
using FireDB.Services;
using PropertyChanged;

namespace FireDB.ViewModels;


[AddINotifyPropertyChangedInterface]
public class StudentViewModel
{
    FireDBService _fireDBService;

    public ObservableCollection<StudentModel> Students { get; set; } = [];
    public StudentModel CurrentStudent { get; set; }

    public ICommand Reset { get; set; }
    public ICommand AddOrUpdateCommand { get; set; }
    public ICommand DeleteCommand { get; set; }

    public StudentViewModel(FireDBService fireDBService)
    {
        this._fireDBService = fireDBService;
        this.Refresh();
        Reset = new Command( async () =>
        {
            CurrentStudent = new StudentModel();
            await this.Refresh();
        }
        );
        AddOrUpdateCommand = new Command(async () =>
        {
            await this.Save();
            await this.Refresh();
        });
        DeleteCommand = new Command(async () =>
        {
            await this.Delete();
            await this.Refresh();
        });

    }
    public async Task GetAll()
    {
        Students = [];
        var items = await _fireDBService.GetAllStudent();
        foreach (var item in items)
        {
            Students.Add(item);
        }
    }
    public async Task Save()
    {
       if(string.IsNullOrEmpty(CurrentStudent.Id))
       {
            await _fireDBService.InsertStudent(this.CurrentStudent);
       }
       else{
            await _fireDBService.UpdateStudent(this.CurrentStudent);
       }
    }
    private async Task Refresh()
    {
        CurrentStudent = new StudentModel();
        await this.GetAll();
    }

    private async Task Delete()
    {
        await _fireDBService.DeleteStudent(this.CurrentStudent.Id);
    }


}
