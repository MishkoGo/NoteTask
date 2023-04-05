using System;
using System.Collections.Generic;

namespace NoteApplication.Models;

public partial class Notes
{
    public Guid Id { get; set; }

    public string? TextNote { get; set; }

    public DateTime? DateNote { get; set; }
}
