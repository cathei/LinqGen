// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

public sealed class GetEnumeratorEvaluation : Evaluation
{
    public GetEnumeratorEvaluation(in LinqGenExpression expression, uint id) : base(expression, id)
    {
    }

    public override void AddUpstream(Generation upstream)
    {
        upstream.HasEnumerator = true;
        base.AddUpstream(upstream);
    }
}