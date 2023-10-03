namespace SudokuGame.Viewmodel
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseViewmodel
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static BaseViewmodel _instance;

        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static BaseViewmodel Instance
        {
            set => _instance = value;
            get
            {
                return _instance = _instance ?? new BaseViewmodel();
            }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="BaseViewmodel"/> class from being created.
        /// </summary>
        public BaseViewmodel()
        {


        }
    }
}
