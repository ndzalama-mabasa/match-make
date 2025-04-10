using System.Collections.Generic;
using ReactiveUI;

public class Interest : ReactiveObject
{
    private int _id;
    private string _interestName;

    private IReadOnlyList<UserInterest> _userInterests;

    public Interest()
    {
        _userInterests = new List<UserInterest>().AsReadOnly();
    }


    public int Id
    {
        get => _id;
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }

    public string InterestName
    {
        get => _interestName;
        set => this.RaiseAndSetIfChanged(ref _interestName, value);
    }

    public IReadOnlyList<UserInterest> UserInterests
    {
        get => _userInterests;
        set => this.RaiseAndSetIfChanged(ref _userInterests, value);
    }
}