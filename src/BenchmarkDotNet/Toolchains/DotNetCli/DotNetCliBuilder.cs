using System;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.Results;
using JetBrains.Annotations;

namespace BenchmarkDotNet.Toolchains.DotNetCli
{
    [PublicAPI]
    public class DotNetCliBuilder : IBuilder
    {
        private string TargetFrameworkMoniker { get; }

        private string CustomDotNetCliPath { get; }
        private bool LogOutput { get; }

        [PublicAPI]
        public DotNetCliBuilder(string targetFrameworkMoniker, string customDotNetCliPath = null, bool logOutput = false)
        {
            TargetFrameworkMoniker = targetFrameworkMoniker;
            CustomDotNetCliPath = customDotNetCliPath;
            LogOutput = logOutput;
        }

        public BuildResult Build(GenerateResult generateResult, BuildPartition buildPartition, ILogger logger)
            => new DotNetCliCommand(
                    CustomDotNetCliPath,
                    string.Empty,
                    generateResult,
                    logger,
                    buildPartition,
                    Array.Empty<EnvironmentVariable>(),
                    buildPartition.Timeout,
                    logOutput: LogOutput)
                .RestoreThenBuild();
    }
}
