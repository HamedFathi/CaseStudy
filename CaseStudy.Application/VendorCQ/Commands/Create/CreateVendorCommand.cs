﻿using HamedStack.CQRS;

namespace CaseStudy.Application.VendorCQ.Commands.Create;

public class CreateVendorCommand : ICommand<int>
{
    public string Name { get; set; }
    public string Name2 { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Zip { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Mail { get; set; }
    public string Phone { get; set; }
    public string Notes { get; set; }
}