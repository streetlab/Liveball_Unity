//
//  Message.h
//  JiverExample
//
//  Created by JIVER Developers on 2015. 3. 2. in San Francisco, CA.
//  Copyright (c) 2015년 JIVER.CO. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "Sender.h"
#import "OrderedModel.h"

@interface Message : OrderedModel

@property (retain) NSString *message;
@property (retain) Sender *sender;
@property long long timestamp;
@property BOOL isOpMessage;
@property BOOL isGuestMessage;
@property (retain) NSString *data;
@property (retain) NSDictionary *jsonObj;

- (id) initWithDic:(NSDictionary *)dic inPresent:(BOOL)present;
- (BOOL) hasSameSender:(Message *)msg;
- (NSString *)getSenderName;
- (void) mergeWith:(Message *)merge;
- (NSString *) toJson;

@end
