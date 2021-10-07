using HotChocolate.Data;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Types;

public class CustomFilteringConvention : FilterConvention
{
    protected override void Configure(IFilterConventionDescriptor descriptor)
    {
        descriptor.AddDefaults();
        descriptor.Provider(
            new QueryableFilterProvider(x => x
                    .AddDefaultFieldHandlers()
                    .AddFieldHandler<QueryableStringInvariantContainsHandler>()));
                    descriptor
            .Configure<StringOperationFilterInputType>(_ => _
                .Operation(DefaultFilterOperations.Contains)
                .Type<StringType>()
                .Extend()
                .OnBeforeCreate(p => p.Handler = new QueryableStringInvariantContainsHandler()));
    }
}