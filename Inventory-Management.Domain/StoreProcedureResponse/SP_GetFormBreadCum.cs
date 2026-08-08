namespace Inventory_Management.Domain.StoreProcedureResponse;

public class SP_GetFormBreadCum
{
    public int FormId { get; set; }

    public string FormName { get; set; }

    public string FormCode { get; set; }

    public int ParentId { get; set; }

    public string BreadCum { get; set; }
    public string FormUrl { get; set; }
    public int DisplayIndex { get; set; }
}

