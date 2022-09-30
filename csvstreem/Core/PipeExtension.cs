public static class PipeExtension {
  public static TOutput Pipe<TInput, TOutput>(this TInput input, in Func<TInput, TOutput> map)
    where TInput : class
    where TOutput : class =>
      map(input);
}