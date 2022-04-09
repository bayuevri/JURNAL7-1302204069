using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace JURNAL7_1302204069
{
	internal class BankTransferConfig
	{
			public string Language { get; set; }

			public int TransferThreshold { get; set; }
			public int TransferLowFee { get; set; }
			public int TransferHighFee { get; set; }

			public List<string> Methods { get; set; }

			public string Confirmation { get; set; }


			// Menyetting nilai property saat instance dibuat
			public BankTransferConfig()
			{
				Methods = new List<string>();
				ReadFile();
			}

			// Melakukan user input
			public void TryTransfer()
			{
				if (Language == "en")
				{
					Console.WriteLine("Please insert the amount of money to transfer");
				}
				else
				{
					Console.WriteLine("Masukkan jumlah uang yang akan di-transfer:");
				}
				string rawTransfer = Console.ReadLine();
				int transfer = int.Parse(rawTransfer);
				int biayaTransfer;

				int totalBiaya;

				if (transfer <= TransferThreshold)
				{
					biayaTransfer = TransferLowFee;
				}
				else
				{
					biayaTransfer = TransferHighFee;
				}

				totalBiaya = transfer + biayaTransfer;

				if (Language == "en")
				{
					Console.WriteLine($"Transfer fee: {biayaTransfer}");
					Console.WriteLine($"Total amount: {totalBiaya}");

					Console.WriteLine("\nSelect transfer method: ");
				}
				else
				{
					Console.WriteLine($"Biaya transfer: {biayaTransfer}");
					Console.WriteLine($"Total biaya: {totalBiaya}");

					Console.WriteLine("\nPilih metode transfer: ");
				}

				for (int i = 0; i < Methods.Count; i++)
				{
					Console.WriteLine($"{i + 1}. {Methods[i]}");
				}
				string method = Console.ReadLine();

				if (Language == "en")
				{
					Console.WriteLine($"Please type {Confirmation}");
				}
				else
				{
					Console.WriteLine($"Ketik {Confirmation}");
				}
				string confirm = Console.ReadLine();

				if (confirm == Confirmation)
				{
					if (Language == "en")
					{
						Console.WriteLine("The transfer is completed");
					}
					else
					{
						Console.WriteLine("Proses transfer berhasil");
					}
				}
				else
				{
					if (Language == "en")
					{
						Console.WriteLine("Transfer is cancelled");
					}
					else
					{
						Console.WriteLine("Transfer dibatalkan");
					}
				}

			}

			// Mendapatkan path dari file
			private string GetFilePath => Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "bank_transfer_config.json");

			// Membaca file config json
			public void ReadFile()
			{
				var file = File.OpenText(GetFilePath);

				JsonElement json = JsonSerializer.Deserialize<JsonElement>(file.ReadToEnd());

				Language = json.GetProperty("lang").GetString();

				TransferThreshold = json.GetProperty("transfer").GetProperty("threshold").GetInt32();
				TransferLowFee = json.GetProperty("transfer").GetProperty("low_fee").GetInt32();
				TransferHighFee = json.GetProperty("transfer").GetProperty("high_fee").GetInt32();

				Confirmation = json.GetProperty("confirmation").GetProperty(Language).GetString();

				foreach (var item in json.GetProperty("methods").EnumerateArray().ToList())
				{
					Methods.Add(item.GetString());
				}

			}

		}
	}
