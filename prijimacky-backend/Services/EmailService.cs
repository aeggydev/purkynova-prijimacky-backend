﻿using prijimacky_backend.Data;
using prijimacky_backend.Entities;
using prijimacky_backend.Graphql.Types;
using RazorLight;
using Path = System.IO.Path;

namespace prijimacky_backend.Services;

public class EmailService : IEmailService
{
    private readonly ApplicationDbContext _db;
    private readonly RazorLightEngine _razorEngine;

    public EmailService(ApplicationDbContext db)
    {
        _db = db;
        _razorEngine = new RazorLightEngineBuilder()
            .UseFileSystemProject(Directory.GetCurrentDirectory()+@"\Email")
            .UseMemoryCachingProvider()
            .Build();
    }

    private void DebugHtml(string html)
    {
        var path = Path.GetTempFileName() + ".html";
        File.WriteAllText(path, html);
        System.Diagnostics.Process.Start(@"C:\Program Files\Google\Chrome\Application\chrome.exe",
            Uri.EscapeDataString(path));
    }

    public EmailStatistics GetStatistics()
    {
        var participants = _db.Participants.ToList();
        return new EmailStatistics(
            participants.Count(x => x.Status == ParticipantStatus.PaidConfirmed),
            participants.Count(x => x.Status == ParticipantStatus.PaidUnconfirmed),
            participants.Count(x => x.Status == ParticipantStatus.UnpaidLate),
            participants.Count(x => x.Status == ParticipantStatus.Unpaid),
            participants.Count(x => x.Status == ParticipantStatus.Canceled),
            0); // TODO: Add actual free spot counting
    }

    public async Task<bool> SendPaymentConfirmation(Participant participant)
    {
        var html = await _razorEngine.CompileRenderAsync("ConfirmationEmail.cshtml", participant);
        Console.WriteLine(html);
        DebugHtml(html);
        return true;
    }

    public Task<bool> SendCancelConfirmation(Participant participant)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SendForcedChangeConfirmation(Participant participant, bool status)
    {
        throw new NotImplementedException();
    }
}