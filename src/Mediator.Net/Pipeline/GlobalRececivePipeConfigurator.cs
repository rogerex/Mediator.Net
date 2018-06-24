﻿using System.Collections.Generic;
using System.Linq;
using Mediator.Net.Context;
using Mediator.Net.Contracts;

namespace Mediator.Net.Pipeline
{
    class GlobalRececivePipeConfigurator : IGlobalReceivePipeConfigurator
    {
        private readonly IDependancyScope _resolver;
        private readonly IList<IPipeSpecification<IReceiveContext<IMessage>>> _specifications;
        public IDependancyScope DependancyScope => _resolver;
        public GlobalRececivePipeConfigurator(IDependancyScope resolver = null)
        {
            _resolver = resolver;
            _specifications = new List<IPipeSpecification<IReceiveContext<IMessage>>>();
        }
        public IGlobalReceivePipe<IReceiveContext<IMessage>> Build()
        {
            return Chain();
        }

        public void AddPipeSpecification(IPipeSpecification<IReceiveContext<IMessage>> specification)
        {
            _specifications.Add(specification);
        }

        IGlobalReceivePipe<IReceiveContext<IMessage>> Chain()
        {
            IGlobalReceivePipe<IReceiveContext<IMessage>> current = null;
            if (_specifications.Any())
            {
                for (int i = _specifications.Count - 1; i >= 0; i--)
                {
                    current = i == _specifications.Count - 1 ? new GlobalReceivePipe<IReceiveContext<IMessage>>(_specifications[i], null) : new GlobalReceivePipe<IReceiveContext<IMessage>>(_specifications[i], current);
                }
            }
            else
            {
                current = new GlobalReceivePipe<IReceiveContext<IMessage>>(new EmptyPipeSpecification<IReceiveContext<IMessage>>(), null);
            }

            return current;
        }
    }
}