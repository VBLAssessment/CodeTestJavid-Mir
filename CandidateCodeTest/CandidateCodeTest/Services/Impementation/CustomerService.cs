﻿using CandidateCodeTest.Common.Interfaces;
using CandidateCodeTest.Services;
using System;
using System.Threading.Tasks;

namespace CandidateCodeTest
{
    public class CustomerService : ICustomerService
    {
        public ILogWriter _logWriter;
        public IMessageService _messageService;
        public TimeSpan? _startTime;
        public TimeSpan? _endTime;
        public CustomerService(IMessageService messageService, TimeSpan? startTime, TimeSpan? endTime, ILogWriter logWriter)
        {
            _logWriter = logWriter;
            _startTime = startTime;
            _endTime = endTime;
            _messageService = messageService;
        }
        public async Task<bool> HasEmailBeenSent()
        {
            _logWriter.LogWrite("HasEmailBeenSent Method Started");
            try
            {
                TimeSpan now = DateTime.Now.TimeOfDay;
                if (_startTime == null || _endTime == null)
                {
                    return false;
                }

                if ((now > _startTime) && (now < _endTime))
                {
                    await _messageService.SendEmailAsync();
                    _logWriter.LogWrite("HasEmailBeenSent Method ended");
                    return true;
                }
                else
                {
                    _logWriter.LogWrite("HasEmailBeenSent Method ended, email not sent due to ime constratint");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logWriter.LogWrite($"HasEmailBeenSent Method ended, email not sent, exception message : {ex.Message}");
                return false;
            }
        }
    }


}