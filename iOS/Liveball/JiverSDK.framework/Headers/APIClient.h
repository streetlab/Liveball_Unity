//
//  APIClient.h
//  JiverExample
//
//  Created by JIVER Developers on 2015. 3. 1. in San Francisco, CA.
//  Copyright (c) 2015년 JIVER.CO. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "Jiver.h"
#import "NSDictionary+JSONString.h"

#define kApiGuestLogin @"/v1/guest_login"
#define kApiChannelList @"/v1/channel_list"
#define kApiChannelJoin @"/v1/channel_join"
#define kApiUploadFile @"/v1/upload_file"
#define kApiMessagingStart @"/v1/messaging_start"
#define kApiMessagingEnd @"/v1/messaging_end"
#define kApiMessagingList @"/v1/messaging_list"
#define kApiLoadMoreMessages @"/v1/load_more_messages"
#define kApiMemberList @"/v1/member_list"
#define kApiMessagingJoin @"/v1/messaging_join"

@interface APIClient : NSObject

@property (retain) NSString *appId;
@property (retain) NSString *sessionKey;

- (id) initWithAppId:(NSString *)appId;
- (void) channelListInPage:(int)page withQuery:(NSString *)query withLimit:(int)limit startBlock:(void (^)())onStart endBlock:(void (^)(NSDictionary *response, NSError *error))onEnd;
- (void) guestLoginWithGuestId:(NSString *)guestId andNickname:(NSString *)nickname andUserImageUrl:(NSString *)imageUrl startBlock:(void (^)())onStart endBlock:(void (^)(NSDictionary *response, NSError *error))onEnd;
- (void) post:(NSString *)uri form:(NSMutableDictionary *)form startBlock:(void (^)())onStart endBlock:(void (^)(NSDictionary *response, NSError *error))onEnd;
- (void) joinChannel:(NSString *)channelUrl startBlock:(void (^)())onStart endBlock:(void (^)(NSDictionary *response, NSError *error))onEnd;
- (void) uploadFile:(NSData *)file startBlock:(void (^)())onStart endBlock:(void (^)(NSDictionary *response, NSError *error))onEnd;
- (void) messagingStartWithGuestId:(NSString *)guestId startBlock:(void (^)())onStart endBlock:(void (^)(NSDictionary *response, NSError *error))onEnd;
- (void) messagingJoinWithChannelUrl:(NSString *)channelUrl startBlock:(void (^)())onStart endBlock:(void (^)(NSDictionary *, NSError *))onEnd;
- (void) messagingEndWithChannelUrl:(NSString *)channelUrl startBlock:(void (^)())onStart endBlock:(void (^)(NSDictionary *response, NSError *error))onEnd;;
- (void) messagingListStartBlock:(void (^)())onStart endBlock:(void (^)(NSDictionary *response, NSError *error))onEnd;;
- (void) loadMoreMessagesInChannel:(long long)channelId andMinMessageId:(long long)minMessageId withLimit:(int)limit startBlock:(void (^)())onStart endBlock:(void (^)(NSDictionary *response, NSError *error))onEnd;
- (void) memberListInChannel:(NSString *)channelUrl withPageNum:(int)page withQuery:(NSString *)query withLimit:(int)limit startBlock:(void (^)())onStart endBlock:(void (^)(NSDictionary *response, NSError *error))onEnd;
- (void) cancelAll;

@end
