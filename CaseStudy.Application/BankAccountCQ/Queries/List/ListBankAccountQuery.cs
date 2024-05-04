﻿using HamedStack.CQRS;

namespace CaseStudy.Application.BankAccountCQ.Queries.List;

public class ListBankAccountQuery : IQuery<IEnumerable<BankAcountDTO>>
{
    public int VendorId { get; set; }
    public int? PageIndex { get; set; }
    public int? PageSize { get; set; }
}