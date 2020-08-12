namespace Reach.Pipelines.DataModel
{
    public class PipelineExecutionOptions
    {
        public static PipelineExecutionOptions Default { get; set; }

        static PipelineExecutionOptions()
        {
            Default = new PipelineExecutionOptions
            {
                BreakOnError = true
            };
        }

        /// <summary>
        /// Gets or Sets whether or not to break when an error is encountered
        /// </summary>
        public bool BreakOnError { get; set; }
    }
}
