#import "TubyIAP.h"
#import "Tuby3.h"

extern "C"
{
    void iOSInAppInit()
    {
        [[TubyIAP sharedTubyIAP] initInApp];
    }
    void iOSBuyItem(const char* pszProductId)
    {
        NSString* strProductId = [NSString stringWithUTF8String:pszProductId];
        [[TubyIAP sharedTubyIAP] requestProductData:strProductId];
    }
    void iOSRestoreCompletedTransactions()
    {
        [[TubyIAP sharedTubyIAP] restoreCompletedTransactions];
    }
}

@implementation TubyIAP

+ (TubyIAP*) sharedTubyIAP
{
    static TubyIAP* pInstance;
    if(pInstance == NULL)
    {
        pInstance = [[TubyIAP alloc] init];
    }
    return pInstance;
}

- (BOOL) initInApp
{
    if( [SKPaymentQueue canMakePayments] == NO ){
        UnitySendMessage("IOSMgr", "MsgReceived", "NO");
        return NO;
    }
    
    [[SKPaymentQueue defaultQueue] addTransactionObserver:self];
    
    NSLog(@"InAppPurchase init OK");
    UnitySendMessage("IOSMgr", "MsgReceived", "OK");
    return true;
}

///< 아이템 정보 요청
- (void) requestProductData:(NSString*)strProductId
{
    ///< iTunes Connect에 설정한 Product ID들
//    NSSet* productIdentifiers = [NSSet setWithObject:strProductId];
    NSSet * productIdentifiers = [NSSet setWithObjects:@"ruby_50", @"ruby_100", @"com.streetlab.tuby.ruby_500", @"ruby_500", @"com.streetlab.tuby.ruby_50", nil];
    SKProductsRequest* request = [[SKProductsRequest alloc] initWithProductIdentifiers:productIdentifiers];
    request.delegate = self;
    [request start];
    NSLog(@"requestProductData %@", strProductId);
}

///< 아이템 정보 요청 결과 callback
- (void) productsRequest:(SKProductsRequest *)request didReceiveResponse:(SKProductsResponse *)response
{
    NSLog( @"InAppPurchase didReceiveResponse" );
    for( SKProduct* product in response.products )
    {
        if( product != nil )
        {
            NSLog(@"InAppPurchase Product title: %@", product.localizedTitle);
            NSLog(@"InAppPurchase Product description: %@", product.localizedDescription);
            NSLog(@"InAppPurchase Product price: %@", product.price);
            //product.priceLocale
            NSLog(@"InAppPurchase Product id: %@", product.productIdentifier);
            
            ///< 구매 요청
            
            SKPayment* payment = [SKPayment paymentWithProduct:product];
            //payment.quantity = 10;
            [[SKPaymentQueue defaultQueue] addPayment:payment];
        }
    }
    
//    [request release];
    
    for (NSString *invalidProductId in response.invalidProductIdentifiers)
    {
        NSLog(@"InAppPurchase Invalid product id: %@", invalidProductId);
    }
}

///< 새로운 거래가 발생하거나 갱신될 때 호출된다.
- (void) paymentQueue:(SKPaymentQueue *)queue updatedTransactions:(NSArray *)transactions
{
    for (SKPaymentTransaction *transaction in transactions)
    {
        switch (transaction.transactionState)
        {
                ///< 서버에 거래 처리중
            case SKPaymentTransactionStatePurchasing:
                NSLog(@"InAppPurchase SKPaymentTransactionStatePurchasing");
                break;
                ///< 구매 완료
            case SKPaymentTransactionStatePurchased:
                [self completeTransaction:transaction];
                break;
                ///< 거래 실패 또는 취소
            case SKPaymentTransactionStateFailed:
                [self failedTransaction:transaction];
                break;
                ///< 재구매
            case SKPaymentTransactionStateRestored:
                [self restoreTransaction:transaction];
                break;
        }
    }
}

- (void) completeTransaction:(SKPaymentTransaction *)transaction
{
    NSLog(@"InAppPurchase completeTransaction");
    NSLog(@"InAppPurchase Transaction Identifier : %@", transaction.transactionIdentifier );
    NSLog(@"InAppPurchase Transaction Data : %@", transaction.transactionDate );
    ///< 구매 완료 후 아이템 인벤등 게임쪽 후 처리 진행
    /* 빌트 인 모델
     const char* pszProductId = [[[transaction payment] productIdentifier] UTF8String];
     UnitySendMessage("iOSManager", "ResultBuyItem", pszProductId);
     */
    
    NSString* strReceipt = [[NSString alloc] initWithBytes:transaction.transactionReceipt.bytes length:transaction.transactionReceipt.length encoding:NSUTF8StringEncoding];
    
    UnitySendMessage("iOSManager", "ResultBuyItem", [strReceipt UTF8String]);
    
    // Remove the transaction from the payment queue.
    [[SKPaymentQueue defaultQueue] finishTransaction:transaction];
}

- (void) restoreTransaction:(SKPaymentTransaction *)transaction
{
    NSLog(@"InAppPurchase restoreTransaction");
    const char* pszRestoreProductId = [transaction.originalTransaction.payment.productIdentifier UTF8String];
    UnitySendMessage("iOSManager", "ResultRestoreItem", pszRestoreProductId);
    [[SKPaymentQueue defaultQueue] finishTransaction:transaction];
}

- (void) failedTransaction:(SKPaymentTransaction *)transaction
{
    NSLog(@"InAppPurchase failedTransaction.");
    const char* pszResult = 0;
    if( transaction.error.code != SKErrorPaymentCancelled )
    {
        pszResult = "faileIAP";
        NSLog(@"InAppPurchase failedTransaction SKErrorDomain - %d", transaction.error.code );
    }
    else
    {
        pszResult = "cancelIAP";
        NSLog(@"InAppPurchase failedTransaction SKErrorPaymentCancelled");
    }
    UnitySendMessage("iOSManager", "ResultBuyItem", pszResult);
    [[SKPaymentQueue defaultQueue] finishTransaction:transaction];
}

// 비소모성 아이템 복원 요청
- (void) restoreCompletedTransactions
{
    [[SKPaymentQueue defaultQueue] restoreCompletedTransactions];
}

@end