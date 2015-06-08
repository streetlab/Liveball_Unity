//
//  Jiver.h
//  JiverExample
//
//  Created by JIVER Developers on 2015. 3. 2. in San Francisco, CA.
//  Copyright (c) 2015년 JIVER.CO. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import <AdSupport/AdSupport.h>
#import "JiverClient.h"
#import "FileInfo.h"
#import "ChannelListQuery.h"
#import "APIClient.h"
#import "BroadcastMessage.h"
#import "MessagingChannel.h"
#import "ReadStatus.h"
#import "TypeStatus.h"
#import "MessagingChannelListQuery.h"
#import "MemberListQuery.h"

@class ChannelListQuery;
@class JiverClient;
@class MessagingChannelListQuery;
@class MemberListQuery;

@interface Jiver : NSObject

+ (NSString *) VERSION;
+ (BOOL) LOG_DEBUG;
+ (NSString *) WS_HOST;
+ (NSString *) API_HOST;

+ (Jiver *) sharedInstance;

@property (retain) NSString *appId;
@property BOOL connected;
@property BOOL mLoginRequired;
@property (retain) NSOperationQueue *taskQueue;
@property (retain) NSOperationQueue *imageTaskQueue;

- (id) initWithAppId:(NSString *)appId;
+ (void) initWithAppKey:(NSString *)appKey;

// Initialization For IGAWorks
+ (void) initByIDFVWithBundle:(NSBundle *)bundle andAppId:(NSString *)appId DEPRECATED_ATTRIBUTE;
+ (void) initByIDFAWithBundle:(NSBundle *)bundle andAppId:(NSString *)appId DEPRECATED_ATTRIBUTE;
+ (void) initUserId:(NSString *)userId withBundle:(NSBundle *)bundle andAppId:(NSString *)appId DEPRECATED_ATTRIBUTE;
+ (void) igawInitByIDFVWithBundle:(NSBundle *)bundle andAppId:(NSString *)appId;
+ (void) igawInitByIDFAWithBundle:(NSBundle *)bundle andAppId:(NSString *)appId;
+ (void) igawInitUserId:(NSString *)userId withBundle:(NSBundle *)bundle andAppId:(NSString *)appId;

+ (void) loginWithUserName:(NSString *)userName DEPRECATED_ATTRIBUTE;
+ (void) loginWithUserName:(NSString *)userName andUserImageUrl:(NSString *)imageUrl DEPRECATED_ATTRIBUTE;
+ (void) igawLoginWithUserName:(NSString *)userName;
+ (void) igawLoginWithUserName:(NSString *)userName andUserImageUrl:(NSString *)imageUrl;
+ (void) loginWithUserId:(NSString *)userId andUserName:(NSString *)userName;
+ (void) loginWithUserId:(NSString *)userId andUserName:(NSString *)userName andUserImageUrl:(NSString *)imageUrl;
+ (void) joinChannel:(NSString *)channelUrl;
+ (void) setLastMessageLimit:(int)limit;

+ (NSString *) getUserId;
+ (NSString *) getUserName;

+ (void) setEventHandlerConnectBlock:(void (^)(Channel *channel))connect errorBlock:(void (^)(int code))error messageReceivedBlock:(void (^)(Message *message))messageReceived systemMessageReceivedBlock:(void (^)(SystemMessage *message))systemMessageReceived broadcastMessageReceivedBlock:(void (^)(BroadcastMessage *message))broadcastMessageReceived fileReceivedBlock:(void (^)(FileLink *fileLink))fileReceived messagingStartedBlock:(void (^)(MessagingChannel *channel))messagingStarted messagingEndedBlock:(void (^)(MessagingChannel *channel))messagingEnded readReceivedBlock:(void (^)(ReadStatus *status))readReceived typeStartReceivedBlock:(void (^)(TypeStatus *status))typeStartReceived typeEndReceivedBlock:(void (^)(TypeStatus *status))typeEndReceived messagesLoadedBlock:(void (^)(int count))messagesLoaded allDataReceivedBlock:(void (^)())allDataReceived;

+ (Channel *) getCurrentChannel;

+ (void) startMessagingWithUserId:(NSString *)userId;
+ (void) joinMessagingWithChannelUrl:(NSString *)channelUrl;
+ (void) endMessagingWithChannelUrl:(NSString *)channelUrl;

+ (void) typeStart;
+ (void) typeEnd;
+ (void) markAsRead;

+ (void) connect;
+ (void) disconnect;

+ (void) sendMessage:(NSString *)message;
+ (void) sendMessage:(NSString *)message withData:(NSString *)data;
+ (void) sendFile:(FileInfo *)fileInfo;
+ (void) uploadFile:(NSData *)file type:(NSString *)type hasSizeOfFile:(unsigned long)size withCustomField:(NSString *)customField uploadBlock:(void (^)(FileInfo *fileInfo, NSError *error))onUpload;
+ (void) loadMoreMessagesWithLimit:(int)limit;

+ (MessagingChannelListQuery *) queryMessagingChannelList;
+ (ChannelListQuery *) queryChannelList;
+ (ChannelListQuery *) queryChannelListWithKeyword:(NSString *)keyword;
+ (ChannelListQuery *) queryChannelListForUnity;
+ (MemberListQuery *) queryMemberListInChannel:(NSString *)channelUrl;

+ (void) setLastMessageMills:(long long)lastMessageMills;
+ (long long) getLastMessageMills;

+ (void) messageReceived:(Message *)msg DEPRECATED_ATTRIBUTE;
+ (void) fileReceived:(FileLink *)fileLink DEPRECATED_ATTRIBUTE;
+ (void) broadcastMessageReceived:(BroadcastMessage *)msg DEPRECATED_ATTRIBUTE;
+ (void) systemMessageReceived:(SystemMessage *)msg DEPRECATED_ATTRIBUTE;
+ (void) messagingStarted:(MessagingChannel *)channel DEPRECATED_ATTRIBUTE;
+ (void) messagingEnded:(MessagingChannel *)channel DEPRECATED_ATTRIBUTE;

+ (NSString *) deviceUniqueID;

@end
