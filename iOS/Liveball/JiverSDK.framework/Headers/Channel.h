//
//  Channel.h
//  JiverExample
//
//  Created by JIVER Developers on 2015. 3. 2. in San Francisco, CA.
//  Copyright (c) 2015년 JIVER.CO. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "OrderedModel.h"

@interface Channel : OrderedModel

@property long long channelId;
@property int memberCount;
@property (retain) NSString *url;
@property (retain) NSString *name;
@property (retain) NSString *coverUrl;
@property (retain) NSDictionary *jsonObj;

- (id) initWithDic:(NSDictionary *) dic;
- (NSString *) getUrlWithoutAppPrefix;
- (NSString *) toJson;

@end
