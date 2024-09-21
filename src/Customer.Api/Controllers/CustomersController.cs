using Customer.Api.ApiModels.Customer;
using Customer.Api.ApiModels.Response;
using Customer.Application.UseCases.Customer.AddAddress;
using Customer.Application.UseCases.Customer.Common;
using Customer.Application.UseCases.Customer.CreateCustomer;
using Customer.Application.UseCases.Customer.DeleteAddresses;
using Customer.Application.UseCases.Customer.DeleteCustomer;
using Customer.Application.UseCases.Customer.GetCustomer;
using Customer.Application.UseCases.Customer.ListCustomers;
using Customer.Application.UseCases.Customer.UpdateAddress;
using Customer.Application.UseCases.Customer.UpdateCustomer;
using Customer.Domain.SeedWork.SearchableRepository;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace Customer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController(
    IMediator mediator)
    : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CustomerModelOutput>),
        StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create(
        [FromBody] CreateCustomerInput input,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var output = await mediator.Send(input, cancellationToken);

            return CreatedAtAction(
                nameof(Create),
                new { output.Id },
                new ApiModels.Response.ApiResponse<CustomerModelOutput>(output)
            );
        }
        catch (OperationCanceledException)
        {
            return
                StatusCode(499,
                    "Operation canceled for user");
        }
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<CustomerModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update(
        [FromBody] UpdateCustomerApiInput apiInput,
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var input = new UpdateCustomerInput(
                id,
                apiInput.Name,
                apiInput.BirthDate,
                apiInput.GenderType
            );
            var output = await mediator.Send(input, cancellationToken);
            return Ok(new ApiResponse<CustomerModelOutput>(output));
        }
        catch (OperationCanceledException)
        {
            return
                StatusCode(499,
                    "Operation canceled for user");
        }
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<CustomerModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var output = await mediator.Send(new GetCustomerInput(id), cancellationToken);
            return Ok(new ApiResponse<CustomerModelOutput>(output));
        }
        catch (OperationCanceledException)
        {
            return
                StatusCode(499,
                    "Operation canceled for user");
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(ListCustomersOutput), StatusCodes.Status200OK)]
    public async Task<IActionResult> List(
        CancellationToken cancellationToken,
        [FromQuery] int? page = null,
        [FromQuery(Name = "per_page")] int? perPage = null,
        [FromQuery] string? search = null,
        [FromQuery] string? sort = null,
        [FromQuery] SearchOrder? dir = null
    )
    {
        try
        {
            var input = new ListCustomersInput();
            if (page is not null) input.Page = page.Value;
            if (perPage is not null) input.PerPage = perPage.Value;
            if (!String.IsNullOrWhiteSpace(search)) input.Search = search;
            if (!String.IsNullOrWhiteSpace(sort)) input.Sort = sort;
            if (dir is not null) input.Dir = dir.Value;

            var output = await mediator.Send(input, cancellationToken);

            return Ok(
                new ApiResponseList<CustomerModelOutput>(output)
            );
        }
        catch (OperationCanceledException)
        {
            return
                StatusCode(499,
                    "Operation canceled for user");
        }
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        try
        {
            await mediator.Send(new DeleteCustomerInput(id), cancellationToken);
            return NoContent();
        }
        catch (OperationCanceledException)
        {
            return
                StatusCode(499,
                    "Operation canceled for user");
        }
    }

    [HttpPost("{customerId}/addresses")]
    [ProducesResponseType(typeof(ApiResponse<CustomerModelOutput>),
        StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> AddAddresses(
        [FromBody] AddAddressApiInput apiInput,
        [FromRoute] Guid customerId,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var input = new AddAddressInput(
                customerId,
                apiInput.AddressModelInputs.Select(apiAddress => new AddAddressModelInput(
                    apiAddress.Street,
                    apiAddress.Number,
                    apiAddress.Complement,
                    apiAddress.Neighborhood,
                    apiAddress.City,
                    apiAddress.State,
                    apiAddress.ZipCode
                )).ToList()
            );

            var output = await mediator.Send(input, cancellationToken);

            return CreatedAtAction(
                nameof(AddAddresses),
                new { output.Id },
                new ApiModels.Response.ApiResponse<CustomerModelOutput>(output)
            );
        }
        catch (OperationCanceledException)
        {
            return
                StatusCode(499,
                    "Operation canceled for user");
        }
    }

    [HttpDelete("{customerId}/addresses")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAddresses(
        [FromBody] List<int> addressIds,
        [FromRoute] Guid customerId,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var input = new DeleteAddressesInput(
                addressIds,
                customerId
            );

            await mediator.Send(input, cancellationToken);
            return NoContent();
        }
        catch (OperationCanceledException)
        {
            return
                StatusCode(499,
                    "Operation canceled for user");
        }
    }

    [HttpPut("{customerId}/addresses/{addressId}")]
    [ProducesResponseType(typeof(ApiResponse<CustomerModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateAddress(
        [FromBody] UpdateAddressApiInput apiInput,
        [FromRoute] Guid customerId,
        [FromRoute] int addressId,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var input = new UpdateAddressInput(
                customerId,
                new UpdateAddressRequest(
                    addressId,
                    apiInput.Street,
                    apiInput.Number,
                    apiInput.Complement,
                    apiInput.Neighborhood,
                    apiInput.City,
                    apiInput.State,
                    apiInput.ZipCode
                )
            );
            var output = await mediator.Send(input, cancellationToken);
            return Ok(new ApiResponse<CustomerModelOutput>(output));
        }
        catch (OperationCanceledException)
        {
            return
                StatusCode(499,
                    "Operation canceled for user");
        }
    }
}