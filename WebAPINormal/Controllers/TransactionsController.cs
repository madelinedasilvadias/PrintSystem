﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Models;
using WebAPINormal.Extension;
using WebAPINormal.Models;

namespace WebAPINormal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly SchoolContext _context;
        private const decimal PricePerPage = 0.08m;

        public TransactionsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionM>> GetTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            var transactionM = transaction.ToModel();
            return transactionM;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionM>>> GetTransactions()
        {
            var ListOfTransactions = await _context.Transactions.ToListAsync();
            List<TransactionM> ListOfTransactionsM = new List<TransactionM>();
            foreach (Transaction transaction in ListOfTransactions)
            {
                TransactionM transactionM = new TransactionM();
                transactionM = transaction.ToModel();
                ListOfTransactionsM.Add(transactionM);
            }
            return ListOfTransactionsM;
        }

        // PUT: api/Transactions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, Transaction transaction)
        {
            if (id != transaction.TransactionID)
            {
                return BadRequest();
            }

            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Transactions
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(TransactionM transactionM)
        {
            Transaction transaction = transactionM.ToDAL();

            var account = await _context.Accounts.FindAsync(transaction.AccountID);
            if (account == null)
            {
                return NotFound();
            }

            account.StudentBalance += transaction.Amount;

            _context.Transactions.Add(transaction);

            await _context.SaveChangesAsync();

            var transactionM2 = transaction.ToModel();
            return CreatedAtAction(nameof(GetTransaction), new { id = transaction.TransactionID }, transactionM2);
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionID == id);
        }

        [HttpGet("balance/{accountId}")]
        public async Task<ActionResult<decimal>> GetBalance(int accountId)
        {
            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null)
            {
                return NotFound();
            }

            return account.StudentBalance;
        }
        // POST: api/Transactions/Print
        [HttpPost("print")]
        public async Task<ActionResult> Print(PrintRequestM printRequest)
        {
            var account = await _context.Accounts.FindAsync(printRequest.AccountID);
            if (account == null)
            {
                return NotFound();
            }

            var cost = printRequest.NumberOfPages * PricePerPage;
            if (account.StudentBalance < cost)
            {
                return BadRequest("Insufficient balance");
            }

            account.StudentBalance -= cost;

            var transaction = new Transaction
            {
                AccountID = printRequest.AccountID,
                Amount = -cost,
                Date = DateTime.Now,
                StudentBalance = account.StudentBalance
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
