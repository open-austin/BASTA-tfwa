// The QueryableStringOperationHandler already has an implemenation of CanHandle
// It checks if the field is declared in a string operation type and also checks if
// the operation of this field uses the `Operation` specified in the override property further
// below
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Language;

public class QueryableStringInvariantContainsHandler : QueryableStringOperationHandler
{
    // For creating a expression tree we need the `MethodInfo` of the `ToLower` method of string
    private static readonly MethodInfo _toLower = typeof(string)
        .GetMethods()
        .Single(
            x => x.Name == nameof(string.ToLower) &&
            x.GetParameters().Length == 0);
    private static readonly MethodInfo _contains = typeof(string)
.GetMethods()
.Single(x => x.Name == nameof(string.Contains) && x.GetParameters().Any(p => p.ParameterType.Name == nameof(String)) && x.GetParameters().Length == 2);

    // This is used to match the handler to all `contains` fields
    protected override int Operation => DefaultFilterOperations.Contains;

    public override Expression HandleOperation(
        QueryableFilterContext context,
        IFilterOperationField field,
        IValueNode value,
        object parsedValue)
    {
        // We get the instance of the context. This is the expression path to the propert
        // e.g. ~> y.Street
        Expression property = context.GetInstance();

        // the parsed value is what was specified in the query
        // e.g. ~> eq: "221B Baker Street"
        if (parsedValue is string str)
        {
            // Creates and returnes the operation
            // e.g. ~> y.Street.ToLower().Contains("221b baker street")
            // return Expression.Lambda<Func<string, bool>>(
            // Expression.Call(Expression.Call(property, _toLower), _contains, Expression.Constant(str.ToLower()))
            // );
            return Expression.Equal(
            property ,Expression.Constant(str.ToLower())
            );
        }

        // Something went wrong 😱
        throw new InvalidOperationException();
    }
}
