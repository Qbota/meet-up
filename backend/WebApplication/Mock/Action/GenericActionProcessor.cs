using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace WebApplication.Mock.Action
{
    public class GenericActionProcessor
    {

        private readonly ILogger<GenericActionProcessor> _logger;

        public GenericActionProcessor(ILogger<GenericActionProcessor> logger)
        {
            _logger = logger;
        }
        
        public void Process(IEnumerable<IAction> actions)
        {
            foreach (var action in actions)
            {
                Process(action);
            }
        }

        public void Process(IAction action)
        {
            try
            {
                action.Execute();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception during execution of action" + nameof(action));
            }
        }
    }
}