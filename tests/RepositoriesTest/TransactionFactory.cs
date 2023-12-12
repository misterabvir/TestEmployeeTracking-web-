﻿using System.Transactions;

namespace RepositoriesTest;

internal class TransactionFactory
{
    public static TransactionScope CreateTransaction(int seconds = 5)
    {
        return new TransactionScope(
            TransactionScopeOption.Required,
            new TimeSpan(0, 0, seconds),
            TransactionScopeAsyncFlowOption.Enabled);
    }
}
