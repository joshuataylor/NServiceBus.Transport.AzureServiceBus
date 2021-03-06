﻿namespace NServiceBus
{
    using System;
    using Configuration.AdvancedExtensibility;
    using Microsoft.Azure.ServiceBus;
    using Microsoft.Azure.ServiceBus.Primitives;
    using Transport.AzureServiceBus;

    /// <summary>
    /// Adds access to the Azure Service Bus transport config to the global Transport object.
    /// </summary>
    public static class AzureServiceBusTransportSettingsExtensions
    {
        /// <summary>
        /// TODO:
        /// </summary>
        /// <param name="transportExtensions"></param>
        /// <param name="topicName"></param>
        public static TransportExtensions<AzureServiceBusTransport> TopicName(this TransportExtensions<AzureServiceBusTransport> transportExtensions, string topicName)
        {
            Guard.AgainstNullAndEmpty(nameof(topicName), topicName);

            transportExtensions.GetSettings().Set(SettingsKeys.TopicName, topicName);

            return transportExtensions;
        }

        /// <summary>
        /// TODO:
        /// </summary>
        /// <param name="transportExtensions"></param>
        /// <param name="maximumSizeInGB"></param>
        public static TransportExtensions<AzureServiceBusTransport> EntityMaximumSize(this TransportExtensions<AzureServiceBusTransport> transportExtensions, int maximumSizeInGB)
        {
            Guard.AgainstNegativeAndZero(nameof(maximumSizeInGB), maximumSizeInGB);

            transportExtensions.GetSettings().Set(SettingsKeys.MaximumSizeInGB, maximumSizeInGB);

            return transportExtensions;
        }

        /// <summary>
        /// TODO:
        /// </summary>
        public static TransportExtensions<AzureServiceBusTransport> EnablePartitioning(this TransportExtensions<AzureServiceBusTransport> transportExtensions)
        {
            transportExtensions.GetSettings().Set(SettingsKeys.EnablePartitioning, true);

            return transportExtensions;
        }

        /// <summary>
        /// Specifies the multiplier to apply to the maximum concurrency value to calculate the prefetch count.
        /// </summary>
        /// <param name="transportExtensions"></param>
        /// <param name="prefetchMultiplier">The multiplier value to use in the prefetch calculation.</param>
        public static TransportExtensions<AzureServiceBusTransport> PrefetchMultiplier(this TransportExtensions<AzureServiceBusTransport> transportExtensions, int prefetchMultiplier)
        {
            Guard.AgainstNegativeAndZero(nameof(prefetchMultiplier), prefetchMultiplier);

            transportExtensions.GetSettings().Set(SettingsKeys.PrefetchMultiplier, prefetchMultiplier);

            return transportExtensions;
        }

        /// <summary>
        /// Overrides the default prefetch count calculation with the specified value.
        /// </summary>
        /// <param name="transportExtensions"></param>
        /// <param name="prefetchCount">The prefetch count to use.</param>
        public static TransportExtensions<AzureServiceBusTransport> PrefetchCount(this TransportExtensions<AzureServiceBusTransport> transportExtensions, int prefetchCount)
        {
            Guard.AgainstNegativeAndZero(nameof(prefetchCount), prefetchCount);

            transportExtensions.GetSettings().Set(SettingsKeys.PrefetchCount, prefetchCount);

            return transportExtensions;
        }

        /// <summary>
        /// Overrides the default time to wait before triggering a circuit breaker that initiates the endpoint shutdown procedure when the message pump cannot successfully receieve a message.
        /// </summary>
        /// <param name="transportExtensions"></param>
        /// <param name="timeToWait">The time to wait before triggering the circuit breaker.</param>
        public static TransportExtensions<AzureServiceBusTransport> TimeToWaitBeforeTriggeringCircuitBreaker(this TransportExtensions<AzureServiceBusTransport> transportExtensions, TimeSpan timeToWait)
        {
            Guard.AgainstNegativeAndZero(nameof(timeToWait), timeToWait);

            transportExtensions.GetSettings().Set(SettingsKeys.TimeToWaitBeforeTriggeringCircuitBreaker, timeToWait);

            return transportExtensions;
        }

        /// <summary>
        /// TODO: 
        /// </summary>
        /// <param name="transportExtensions"></param>
        /// <param name="subscriptionNameShortener"></param>
        /// <returns></returns>
        public static TransportExtensions<AzureServiceBusTransport> SubscriptionNameShortener(this TransportExtensions<AzureServiceBusTransport> transportExtensions, Func<string, string> subscriptionNameShortener)
        {
            Guard.AgainstNull(nameof(subscriptionNameShortener), subscriptionNameShortener);

            Func<string, string> wrappedSubscriptionNameShortener = subsciptionName =>
            {
                try
                {
                    return subscriptionNameShortener(subsciptionName);
                }
                catch (Exception exception)
                {
                    throw new Exception("Custom subscription name shortener threw an exception.", exception);
                }
            };

            transportExtensions.GetSettings().Set(SettingsKeys.SubscriptionNameShortener, wrappedSubscriptionNameShortener);

            return transportExtensions;
        }

        /// <summary>
        /// TODO: 
        /// </summary>
        /// <param name="transportExtensions"></param>
        /// <param name="ruleNameShortener"></param>
        /// <returns></returns>
        public static TransportExtensions<AzureServiceBusTransport> RuleNameShortener(this TransportExtensions<AzureServiceBusTransport> transportExtensions, Func<string, string> ruleNameShortener)
        {
            Guard.AgainstNull(nameof(ruleNameShortener), ruleNameShortener);

            Func<string, string> wrappedRuleNameShortener = ruleName =>
            {
                try
                {
                    return ruleNameShortener(ruleName);
                }
                catch (Exception exception)
                {
                    throw new Exception("Custom rule name shortener threw an exception.", exception);
                }
            };

            transportExtensions.GetSettings().Set(SettingsKeys.RuleNameShortener, wrappedRuleNameShortener);

            return transportExtensions;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="transportExtensions"></param>
        /// <returns></returns>
        public static TransportExtensions<AzureServiceBusTransport> UseWebSockets(this TransportExtensions<AzureServiceBusTransport> transportExtensions)
        {
            transportExtensions.GetSettings().Set(SettingsKeys.TransportType, TransportType.AmqpWebSockets);

            return transportExtensions;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="transportExtensions"></param>
        /// <param name="tokenProvider"></param>
        /// <returns></returns>
        public static TransportExtensions<AzureServiceBusTransport> CustomTokenProvider(this TransportExtensions<AzureServiceBusTransport> transportExtensions, ITokenProvider tokenProvider)
        {
            transportExtensions.GetSettings().Set(SettingsKeys.CustomTokenProvider, tokenProvider);

            return transportExtensions;
        }
    }
}