namespace Viyi.Strings.Build.Builder;

internal interface IBuilder {
    void Build();
    ValueTask BuildAsync();
}

abstract class Builder : IBuilder {
    protected Builder() {
        TargetRoot = this.GetTargetRoot();
    }

    protected string TargetRoot { get; }

    public virtual void Build() { }

    public virtual async ValueTask BuildAsync() {
        await Task.Run(Build);
    }
}
