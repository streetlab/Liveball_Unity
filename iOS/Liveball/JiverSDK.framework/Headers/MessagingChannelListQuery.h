//
//  MessagingChannelListQuery.h
//  JiverExample
//
//  Created by Jed Kyung on 2015. 4. 20..
//  Copyright (c) 2015년 JIVER.CO. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "JiverClient.h"

@class JiverClient;

@interface MessagingChannelListQuery : NSObject

- (id) initWithClient:(JiverClient *)jiverClient;
- (BOOL) isLoading;
- (void) executeWithResultBlock:(void (^)(NSMutableArray *queryResult))onResult endBlock:(void (^)(NSError *error))onError;

@end
