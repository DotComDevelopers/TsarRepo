using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Collections.Generic;


namespace Tsar_Bot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)

        {


            var activity = await result as Activity;



            // calculate something for us to return 

            int length = (activity.Text ?? string.Empty).Length;



            // return our reply to the user 



            //test 

           
          if (activity.Text.Contains("morning"))

            {

                await context.PostAsync("Hello !! Good Morning , Have a nice Day");

            }

            //test 

            else if (activity.Text.Contains("night"))

            {

                await context.PostAsync(" Good night and Sweetest Dreams with Bot Application ");

            }
          else if (activity.Text.Contains("goodbye"))

          {

              await context.PostAsync(" Good Bye and Have a great Day at work Today with Tsar Bot ");

          }

            else if (activity.Text.Contains("date"))

            {

                await context.PostAsync(DateTime.Now.ToString());

            }
            else if (activity.Text.Contains("what kind"))
            {
                await context.PostAsync("Lukker and you");
            }
            else if (activity.Text.Contains("help"))
            {
                await context.PostAsync("Call 911");
            }
            
            else if (activity.Text.Contains("show me tsar web"))
            {
                await context.PostAsync("http://tsar2.azurewebsites.net/");
            }
            else if (activity.Text.Contains("What are you"))
            {
                await context.PostAsync("Im a Tsar Bot, Here to help you with anything you require");
            }
          else if (activity.Text.Contains("Call the office"))
          {
              await context.PostAsync("Call 0312623112");
          }
            else
            {

                await context.PostAsync("I Do Not Understand Please Check With www.google.com. Thank you ");

            }

            



            context.Wait(MessageReceivedAsync);

        }
    }
}