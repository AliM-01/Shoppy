using IM.Application.Contracts.Inventory.DTOs;
using System;

namespace IM.Application.Contracts.Inventory.Queries;

public record GetInventoryDetailsQuery(Guid Id) : IRequest<Response<EditInventoryDto>>;