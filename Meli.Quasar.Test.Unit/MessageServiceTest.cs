

using AutoMapper;
using FluentAssertions;
using Meli.Quasar.Domain.Exceptions;
using Meli.Quasar.Service;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Meli.Quasar.Test.Unit
{
    public class MessageServiceTest
    {
        //Mock Dependencies

        public Mock<ILogger<MessageService>> ILoggerMock { get; set; }

        public MessageService Sut { get; set; }


        public MessageServiceTest()
        {
            ILoggerMock = new Mock<ILogger<MessageService>>();

            Sut = new MessageService(ILoggerMock.Object);
        }

        [Fact]
        public void When_sends_the_corrects_messages_to_GetMessage_it_should_return_a_correct_message()
         {
            //ARRANGE
            var messages = new List<List<string>> {
                new List<string> { "este", "", "", "mensaje", ""},
                new List<string>{ "", "es", "", "", "secreto"},
                new List<string>{ "este", "", "un", "", ""}
            };

            //ACT
            var message = Sut.GetMessage(messages);

            //ASSERT
            message.Should().Be("este es un mensaje secreto");
        }


        [Fact]
        public void When_sends_diferents_sizes_messages_to_GetMessage_it_should_return_calculate_message_exception()
        {
            //ARRANGE
            var messages = new List<List<string>> {
                new List<string> { "este", "", "", "mensaje", ""},
                new List<string>{ "", "es", "", "", "secreto"},
                new List<string>{ "este", "", "un", "", "","", "extra"}
            };

            //ACT
            Action test = () => Sut.GetMessage(messages);

            //ASSERT
            test.Should().Throw<CalculateMessageException>();
        }


        [Fact]
        public void When_sends_incomplete_messages_to_GetMessage_it_should_return_calculate_message_exception()
        {
            //ARRANGE
            var messages = new List<List<string>> {
                new List<string> { "este", "", "", "mensaje", ""},
                new List<string>{ "", "es", "", "", "secreto"},
                new List<string>{ "este", "", "", "", "",}
            };

            //ACT
            Action test = () => Sut.GetMessage(messages);

            //ASSERT
            test.Should().Throw<CalculateMessageException>();
        }
    }
}
