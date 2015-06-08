//
//  Command.h
//  JiverExample
//
//  Created by JIVER Developers on 2015. 3. 3..
//  Copyright (c) 2015년 JIVER.CO. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "FileInfo.h"

@interface Command : NSObject

@property (retain) NSString *command;
@property (retain) NSString *payload;

- (id) initWithCommand:(NSString *)command andPayload:(NSString *)payload;
- (id) initWithCommand:(NSString *)command andDictionaryPayload:(NSDictionary *)payload;
- (void) decode:(NSString *)command;
- (NSDictionary *) getJson;
- (NSString *) encode;

+ (Command *)parse:(NSString *)data;
+ (Command *)bLoginWithUserKey:(NSString *)userKey;
+ (Command *)bJoinWithChannelId:(NSString *)channelId andLastMessageMillis:(long long)lastMessageMillis withLastMessageLimit:(int)lastMessageLimit;
+ (Command *)bMessageWithChannelId:(long long)channelId andMessage:(NSString *)message andData:(NSString *)data;
+ (Command *)bFileOfChannelWithChannelId:(long long)channelId andFileInfo:(FileInfo *)fileInfo;
+ (Command *)bPing;
+ (Command *)bReadOfChannel:(long long)channelId andTime:(long long)time;
+ (Command *)bTypeStartOfChannel:(long long)channelId andTime:(long long)time;
+ (Command *)bTypeEndOfChannel:(long long)channelId andTime:(long long)time;

@end
