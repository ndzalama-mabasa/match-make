using ReactiveUI;
using System;

public class UserInterest : ReactiveObject
{
    private int _id;
    private int _interestId;
    private Guid _userId;
    private User _user;
    private Interest _interest;

    public int Id
    {
        get => _id;
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }

    public int InterestId
    {
        get => _interestId;
        set => this.RaiseAndSetIfChanged(ref _interestId, value);
    }

    public Guid UserId
    {
        get => _userId;
        set => this.RaiseAndSetIfChanged(ref _userId, value);
    }

    public User User
    {
        get => _user;
        set => this.RaiseAndSetIfChanged(ref _user, value);
    }
    
    public Interest Interest
    {
        get => _interest;
        set => this.RaiseAndSetIfChanged(ref _interest, value);
    }
}