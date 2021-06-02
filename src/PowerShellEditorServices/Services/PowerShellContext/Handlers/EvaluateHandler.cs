﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Management.Automation;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.PowerShell.EditorServices.Services.PowerShell;
using Microsoft.PowerShell.EditorServices.Services.PowerShell.Execution;

namespace Microsoft.PowerShell.EditorServices.Handlers
{
    internal class EvaluateHandler : IEvaluateHandler
    {
        private readonly ILogger _logger;
        private readonly PowerShellExecutionService _executionService;

        public EvaluateHandler(
            ILoggerFactory factory,
            PowerShellExecutionService executionService)
        {
            _logger = factory.CreateLogger<EvaluateHandler>();
            _executionService = executionService;
        }

        public Task<EvaluateResponseBody> Handle(EvaluateRequestArguments request, CancellationToken cancellationToken)
        {
            _executionService.ExecutePSCommandAsync(
                new PSCommand().AddScript(request.Expression),
                new PowerShellExecutionOptions { WriteInputToHost = true, WriteOutputToHost = true, AddToHistory = true, InterruptCommandPrompt = true },
                CancellationToken.None);

            return Task.FromResult(new EvaluateResponseBody
            {
                Result = "",
                VariablesReference = 0
            });
        }
    }
}
