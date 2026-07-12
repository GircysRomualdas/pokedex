using System;
using System.IO;

namespace IntegrationTests;

public class ConsoleCapture : IDisposable {
  private readonly TextWriter original;
  public StringWriter Output { get; }

  public ConsoleCapture() {
    original = Console.Out;
    Output = new StringWriter();
    Console.SetOut(Output);
  }

  public void Dispose() {
    Console.SetOut(original);
  }
}