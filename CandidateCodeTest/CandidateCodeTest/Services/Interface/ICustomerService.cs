namespace CandidateCodeTest.Services
{
    public interface ICustomerService
    {
        /// <summary>
        /// Function used  for confirmation if email has been sent or not.
        /// </summary>
        /// <returns></returns>
        bool HasEmailBeenSent();
    }
}
