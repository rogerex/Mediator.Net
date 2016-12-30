﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mediator.Net.Binding;
using Mediator.Net.Test.CommandHandlers;
using Mediator.Net.Test.Messages;
using Mediator.Net.Test.Middlewares;
using NUnit.Framework;
using TestStack.BDDfy;

namespace Mediator.Net.Test.TestPipeline
{
    class MiddlewaresShouldBeExecutedInOrder : TestBase
    {
        private IMediator _mediator;
        public void GivenAMediatorAndTwoMiddlewares()
        {
            var binding = new List<MessageBinding>() { new MessageBinding( typeof(TestBaseCommand), typeof(TestBaseCommandHandler) )};
            var builder = new MediatorBuilder();
            _mediator = builder.RegisterHandlers(binding)
                .ConfigureReceivePipe(x =>
            {
                x.UseConsoleLogger1();
                x.UseConsoleLogger2();
            })
            .Build();
            
           
        }

        public async Task WhenACommandIsSent()
        {
            await _mediator.SendAsync(new TestBaseCommand(Guid.NewGuid()));
        }

        public void ThenTheMiddlewaresShouldBeExecutedInOrder()
        {
            
        }

        [Test]
        public void Run()
        {
            this.BDDfy();
        }
    }
}