//
//  JiverClient.h
//  JiverExample
//
//  Created by JIVER Developers on 2015. 3. 2. in San Francisco, CA.
//  Copyright (c) 2015년 JIVER.CO. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "Channel.h"
#import "Message.h"
#import "SystemMessage.h"
#import "FileLink.h"
#import "WSClient.h"
#import "APIClient.h"
#import "Jiver.h"
#import "Command.h"
#import "BroadcastMessage.h"
#import "MessagingChannel.h"
#import "ReadStatus.h"
#import "TypeStatus.h"
#import "JiverError.h"
#import "Member.h"

@interface JiverClient : NSObject

- (id) initWithAppId:(NSString *)appId;
- (void) setEventHandlerConnectBlock:(void (^)(Channel *channel))connect errorBlock:(void (^)(int code))error messageReceivedBlock:(void (^)(Message *message))messageReceived systemMessageReceivedBlock:(void (^)(SystemMessage *message))systemMessageReceived broadcastMessageReceivedBlock:(void (^)(BroadcastMessage *message))broadcastMessageReceived fileReceivedBlock:(void (^)(FileLink *fileLink))fileReceived messagingStartedBlock:(void (^)(MessagingChannel *channel))messagingStarted messagingEndedBlock:(void (^)(MessagingChannel *channel))messagingEnded readReceivedBlock:(void (^)(ReadStatus *status))readReceived typeStartReceivedBlock:(void (^)(TypeStatus *status))typeStartReceived typeEndReceivedBlock:(void (^)(TypeStatus *status))typeEndReceived messagesLoadedBlock:(void (^)(int count))messagesLoaded allDataReceivedBlock:(void (^)())allDataReceived;
- (NSString *) getUserID;
- (NSString *) getUserName;
- (void) setLastMessageLimit:(int)limit;
- (void) setLoginInfoWithUserId:(NSString *)userId andUserName:(NSString *)userName andUserImageUrl:(NSString *)imageUrl;
- (void) setChannelUrl:(NSString *)channelUrl;
- (Channel *) getCurrentChannel;
- (void) connect;
- (void) disconnect;
- (void) cmdMessage:(NSString *)message withData:(NSString *)data;
- (void) markAsRead;
- (void) getChannelListInPage:(int)page withQuery:(NSString *)query withLimit:(int)limit startBlock:(void (^)())onStart endBlock:(void (^)(NSDictionary *response, NSError *error))onEnd;
- (void) getMessagingListWithStartBlock:(void (^)())onStart endBlock:(void (^)(NSDictionary *response, NSError *error))onEnd;
//- (void) loginWithAPIEventHandlerStartBlock:(void (^)())onStart endBlock:(void (^)(NSDictionary *response, NSError *error))onEnd;
- (void) uploadFile:(NSData *)file type:(NSString *)type size:(unsigned long)size customField:(NSString *)customField uploadBlock:(void (^)(FileInfo *fileInfo, NSError *error))onUpload;
- (void) cmdFile:(FileInfo *)fileInfo;
- (void) setLastMessageMills:(long long)lastMessageMills;
- (long long) getLastMessageMills;
- (void) messageReceived:(Message *)msg;
- (void) systemMessageReceived:(SystemMessage *)msg;
- (void) broadcastMessageReceived:(BroadcastMessage *)msg;
- (void) fileReceived:(FileLink *)fileLink;
- (void) messagingStarted:(MessagingChannel *)channel;
- (void) messagingEnded:(MessagingChannel *)channel;
- (void) typeStart:(TypeStatus *)status;
- (void) typeEnd:(TypeStatus *)status;
- (void) startMessagingWithGuestId:(NSString *)guestId;
- (void) endMessagingWithChannelUrl:(NSString *)channelUrl;
- (void) cmdRead;
- (void) cmdTypeStart;
- (void) cmdTypeEnd;
- (void) loadMoreMessagesWithLimit:(int)limit;
- (void) getMemberListInChannel:(NSString *)channelUrl withPageNum:(int)page withQuery:(NSString *)query withLimit:(int)limit startBlock:(void (^)())onStart endBlock:(void (^)(NSDictionary *response, NSError *error))onEnd;
- (void) joinMessagingWithChannelUrl:(NSString *)channelUrl;

@end
