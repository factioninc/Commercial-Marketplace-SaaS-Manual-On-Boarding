﻿using System.Threading.Tasks;

namespace CommandCenter.Webhook
{
    public interface IWebhookHandler
    {
        Task ChangePlanAsync(WebhookPayload payload);

        Task ChangeQuantityAsync(WebhookPayload payload);

        Task ReinstatedAsync(WebhookPayload payload);

        Task SuspendedAsync(WebhookPayload payload);

        Task UnsubscribedAsync(WebhookPayload payload);
    }
}