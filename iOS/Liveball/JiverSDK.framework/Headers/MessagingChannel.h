//
//  MessagingChannel.h
//  JiverExample
//
//  Created by Jed Kyung on 2015. 4. 20..
//  Copyright (c) 2015년 JIVER.CO. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "Channel.h"
#import "Message.h"

@interface MessagingChannel : NSObject

@property (retain) Channel *channel;
@property (retain) NSDictionary *jsonObj;
@property (retain) NSMutableArray *members;
@property (retain) NSMutableDictionary *readStatus;
@property int unreadMessageCount;
@property (retain) Message *lastMessage;

- (id) initWithDic:(NSDictionary *) dic;
- (NSString *) toJson;
- (long) getLastReadMillis:(NSString *)userId;
- (long long) getId;
- (NSString *) getUrl;
- (NSString *) getUrlWithoutAppPrefix;
- (NSString *) getCoverUrl;
- (unsigned long) getMemberCount;
- (BOOL) hasLastMessage;

@end
