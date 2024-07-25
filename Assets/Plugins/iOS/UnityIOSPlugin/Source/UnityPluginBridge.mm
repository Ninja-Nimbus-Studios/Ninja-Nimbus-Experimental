//
//  UnityPluginBridge.m
//  UnityiOSPlugin
//
//  Created by Tatsuo Kumamoto on 7/9/24.
//

#import <Foundation/Foundation.h>
#include "UnityFramework/UnityFramework-Swift.h"

extern "C" {
    
#pragma mark - Functions
    
    int _addTwoNumberInIOS(int a , int b) {
       
        int result = [[UnityPlugin shared] AddTwoNumberWithA:(a) b:(b)];
        return result;
    }

    void _vibratePhone(double seconds, bool isVibrating) {
        [[UnityVibrationPlugin shared] VibratePhoneWithSeconds:(seconds) isVibrating:(isVibrating)];
    }
}
