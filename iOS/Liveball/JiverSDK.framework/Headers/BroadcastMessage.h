//
//  BroadcastMessage.h
//  JiverExample
//
//  Created by Jed Kyung on 2015. 4. 10..
//  Copyright (c) 2015년 JIVER.CO. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "OrderedModel.h"

@interface BroadcastMessage : OrderedModel

@property (retain) NSString *message;
@property long long timestamp;
@property (retain) NSDictionary *jsonObj;

- (id) initWithDic:(NSDictionary *)dic inPresent:(BOOL)present;
- (NSString *) toJson;

@end
