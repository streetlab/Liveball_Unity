#import <StoreKit/StoreKit.h>

@interface TubyIAP : NSObject<SKProductsRequestDelegate, SKPaymentTransactionObserver>

+ (TubyIAP*) sharedTubyIAP;
- (BOOL) initInApp;
- (void) requestProductData:(NSString*)strProductId;

- (void) completeTransaction:(SKPaymentTransaction*)transaction;
- (void) restoreTransaction:(SKPaymentTransaction*)transaction;
- (void) failedTransaction:(SKPaymentTransaction*)transaction;

- (void) restoreCompletedTransactions;

@end