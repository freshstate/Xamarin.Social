
using System;
using NUnit.Framework;
using MonoTouch.UIKit;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Xamarin.Social.iOS.Test
{
	[TestFixture]
	public class KeyChainTest
	{
		Random rand = new Random ();

		[Test]
		public void AddAccount ()
		{
			var account = new Account (rand.Next ().ToString (), new Dictionary<string, string> {
				{ "item1", "value1" },
				{ "item2", "value2" },
			});

			var store = AccountStore.Create ();
			store.Save (account, "TestService");

			var newAccount = store
				.FindAccountsForService ("TestService")
				.FirstOrDefault (a => a.Username == account.Username);

			Assert.NotNull (newAccount);

			Assert.AreEqual ("value1", newAccount.Properties["item1"]);
			Assert.AreEqual ("value2", newAccount.Properties["item2"]);
		}

		[Test]
		public void UpdateAccount ()
		{
			var account = new Account (rand.Next ().ToString (), new Dictionary<string, string> {
				{ "item1", "value1" },
				{ "item2", "value2" },
			});

			var store = AccountStore.Create ();
			store.Save (account, "TestService");

			var newAccount = store
				.FindAccountsForService ("TestService")
				.FirstOrDefault (a => a.Username == account.Username);

			Assert.NotNull (newAccount);

			newAccount.Properties["item1"] = "valueA";
			newAccount.Properties["item2"] = "valueB";

			store.Save (newAccount, "TestService");

			var newerAccount = store
				.FindAccountsForService ("TestService")
				.FirstOrDefault (a => a.Username == account.Username);

			Assert.NotNull (newerAccount);

			Assert.AreEqual ("valueA", newerAccount.Properties["item1"]);
			Assert.AreEqual ("valueB", newerAccount.Properties["item2"]);
		}
	}
}