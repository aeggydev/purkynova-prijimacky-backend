﻿namespace prijimacky_backend.Entities;

public class Participant
{
    public string? Id { get; set; }
    public string? ParticipantName { get; set; }
    public string? ParticipantSurname { get; set; }
    public string? ParentName { get; set; }
    public string? ParentSurname { get; set; }
    public string? School { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Ip { get; set; }
    public string? VariableSymbol { get; set; }
    public DateTime? SignUpDate { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? PaidDate { get; set; }
}