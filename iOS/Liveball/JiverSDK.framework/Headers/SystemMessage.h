//
//  SystemMessage.h
//  JiverExample
//
//  Created by JIVER Developers on 2015. 3. 2. in San Francisco, CA.
//  Copyright (c) 2015년 JIVER.CO. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "OrderedModel.h"

@interface SystemMessage : OrderedModel

@property (retain) NSString *message;
@property long long timestamp;
@property (retain) NSDictionary *jsonObj;

- (id) initWithDic:(NSDictionary *)dic inPresent:(BOOL)present;
- (NSString *) toJson;

@end
