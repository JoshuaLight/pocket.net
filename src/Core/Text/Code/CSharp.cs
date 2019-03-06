namespace Pocket.Common
{
  public struct CSharp
  {
    private readonly Code _code;
    private readonly int _indent;

    public CSharp(Code code, int indent)
    {
      _code = code;
      _indent = indent;
    }

    public override string ToString() => _code.ToString();

    public CSharp Text(string text) =>
      With(_code.Text(text));
    public CSharp NewLine() =>
      With(_code.NewLine());

    public Code.Scope Scope() => new Code.Scope(_code,
      x => x.Text("{").NewLine(),
      x => x.Text("}"))
      .With(_code.Indent(_indent));

    public Code.Scope Scope(string header) =>
      Text(header).NewLine().Scope();
    
    public Code.Scope Region(string name) => new Code.Scope(_code,
      x => x.Text($"#region {name}").NewLine(),
      x => x.Text($"#endregion").NewLine());
    
    private CSharp With(Code _) => this;
  }
}