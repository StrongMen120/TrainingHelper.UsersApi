/* tslint:disable */
/* eslint-disable */
/**
 * Users API
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: all
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */


import * as runtime from '../runtime';
import type {
  AddGroupCommandDto,
  AssignedGroupCommandDto,
  GroupDto,
  ProblemDetails,
  UpdateGroupCommandDto,
} from '../models';
import {
    AddGroupCommandDtoFromJSON,
    AddGroupCommandDtoToJSON,
    AssignedGroupCommandDtoFromJSON,
    AssignedGroupCommandDtoToJSON,
    GroupDtoFromJSON,
    GroupDtoToJSON,
    ProblemDetailsFromJSON,
    ProblemDetailsToJSON,
    UpdateGroupCommandDtoFromJSON,
    UpdateGroupCommandDtoToJSON,
} from '../models';

export interface AddToGroupRequest {
    assignedGroupCommandDto?: AssignedGroupCommandDto;
}

export interface CreateGroupRequest {
    addGroupCommandDto?: AddGroupCommandDto;
}

export interface DeleteGroupRequest {
    groupId: number;
}

export interface GetGroupRequest {
    groupId: number;
}

export interface RemoveFromGroupRequest {
    assignedGroupCommandDto?: AssignedGroupCommandDto;
}

export interface UpdateGroupRequest {
    updateGroupCommandDto?: UpdateGroupCommandDto;
}

/**
 * GroupsApi - interface
 * 
 * @export
 * @interface GroupsApiInterface
 */
export interface GroupsApiInterface {
    /**
     * 
     * @param {AssignedGroupCommandDto} [assignedGroupCommandDto] 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof GroupsApiInterface
     */
    addToGroupRaw(requestParameters: AddToGroupRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<GroupDto>>;

    /**
     */
    addToGroup(assignedGroupCommandDto?: AssignedGroupCommandDto, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<GroupDto>;

    /**
     * 
     * @param {AddGroupCommandDto} [addGroupCommandDto] 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof GroupsApiInterface
     */
    createGroupRaw(requestParameters: CreateGroupRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<GroupDto>>;

    /**
     */
    createGroup(addGroupCommandDto?: AddGroupCommandDto, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<GroupDto>;

    /**
     * 
     * @param {number} groupId 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof GroupsApiInterface
     */
    deleteGroupRaw(requestParameters: DeleteGroupRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<GroupDto>>;

    /**
     */
    deleteGroup(groupId: number, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<GroupDto>;

    /**
     * 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof GroupsApiInterface
     */
    getAllGroupRaw(initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<Array<GroupDto>>>;

    /**
     */
    getAllGroup(initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<Array<GroupDto>>;

    /**
     * 
     * @param {number} groupId 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof GroupsApiInterface
     */
    getGroupRaw(requestParameters: GetGroupRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<GroupDto>>;

    /**
     */
    getGroup(groupId: number, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<GroupDto>;

    /**
     * 
     * @param {AssignedGroupCommandDto} [assignedGroupCommandDto] 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof GroupsApiInterface
     */
    removeFromGroupRaw(requestParameters: RemoveFromGroupRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<GroupDto>>;

    /**
     */
    removeFromGroup(assignedGroupCommandDto?: AssignedGroupCommandDto, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<GroupDto>;

    /**
     * 
     * @param {UpdateGroupCommandDto} [updateGroupCommandDto] 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof GroupsApiInterface
     */
    updateGroupRaw(requestParameters: UpdateGroupRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<GroupDto>>;

    /**
     */
    updateGroup(updateGroupCommandDto?: UpdateGroupCommandDto, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<GroupDto>;

}

/**
 * 
 */
export class GroupsApi extends runtime.BaseAPI implements GroupsApiInterface {

    /**
     */
    async addToGroupRaw(requestParameters: AddToGroupRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<GroupDto>> {
        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json';

        if (this.configuration && this.configuration.accessToken) {
            const token = this.configuration.accessToken;
            const tokenString = await token("BearerJWT", []);

            if (tokenString) {
                headerParameters["Authorization"] = `Bearer ${tokenString}`;
            }
        }
        const response = await this.request({
            path: `/api/v1/groups/add-to-group`,
            method: 'POST',
            headers: headerParameters,
            query: queryParameters,
            body: AssignedGroupCommandDtoToJSON(requestParameters.assignedGroupCommandDto),
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => GroupDtoFromJSON(jsonValue));
    }

    /**
     */
    async addToGroup(assignedGroupCommandDto?: AssignedGroupCommandDto, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<GroupDto> {
        const response = await this.addToGroupRaw({ assignedGroupCommandDto: assignedGroupCommandDto }, initOverrides);
        return await response.value();
    }

    /**
     */
    async createGroupRaw(requestParameters: CreateGroupRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<GroupDto>> {
        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json';

        if (this.configuration && this.configuration.accessToken) {
            const token = this.configuration.accessToken;
            const tokenString = await token("BearerJWT", []);

            if (tokenString) {
                headerParameters["Authorization"] = `Bearer ${tokenString}`;
            }
        }
        const response = await this.request({
            path: `/api/v1/groups/group`,
            method: 'POST',
            headers: headerParameters,
            query: queryParameters,
            body: AddGroupCommandDtoToJSON(requestParameters.addGroupCommandDto),
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => GroupDtoFromJSON(jsonValue));
    }

    /**
     */
    async createGroup(addGroupCommandDto?: AddGroupCommandDto, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<GroupDto> {
        const response = await this.createGroupRaw({ addGroupCommandDto: addGroupCommandDto }, initOverrides);
        return await response.value();
    }

    /**
     */
    async deleteGroupRaw(requestParameters: DeleteGroupRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<GroupDto>> {
        if (requestParameters.groupId === null || requestParameters.groupId === undefined) {
            throw new runtime.RequiredError('groupId','Required parameter requestParameters.groupId was null or undefined when calling deleteGroup.');
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        if (this.configuration && this.configuration.accessToken) {
            const token = this.configuration.accessToken;
            const tokenString = await token("BearerJWT", []);

            if (tokenString) {
                headerParameters["Authorization"] = `Bearer ${tokenString}`;
            }
        }
        const response = await this.request({
            path: `/api/v1/groups/group/{groupId}`.replace(`{${"groupId"}}`, encodeURIComponent(String(requestParameters.groupId))),
            method: 'DELETE',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => GroupDtoFromJSON(jsonValue));
    }

    /**
     */
    async deleteGroup(groupId: number, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<GroupDto> {
        const response = await this.deleteGroupRaw({ groupId: groupId }, initOverrides);
        return await response.value();
    }

    /**
     */
    async getAllGroupRaw(initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<Array<GroupDto>>> {
        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        if (this.configuration && this.configuration.accessToken) {
            const token = this.configuration.accessToken;
            const tokenString = await token("BearerJWT", []);

            if (tokenString) {
                headerParameters["Authorization"] = `Bearer ${tokenString}`;
            }
        }
        const response = await this.request({
            path: `/api/v1/groups/groups`,
            method: 'GET',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => jsonValue.map(GroupDtoFromJSON));
    }

    /**
     */
    async getAllGroup(initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<Array<GroupDto>> {
        const response = await this.getAllGroupRaw(initOverrides);
        return await response.value();
    }

    /**
     */
    async getGroupRaw(requestParameters: GetGroupRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<GroupDto>> {
        if (requestParameters.groupId === null || requestParameters.groupId === undefined) {
            throw new runtime.RequiredError('groupId','Required parameter requestParameters.groupId was null or undefined when calling getGroup.');
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        if (this.configuration && this.configuration.accessToken) {
            const token = this.configuration.accessToken;
            const tokenString = await token("BearerJWT", []);

            if (tokenString) {
                headerParameters["Authorization"] = `Bearer ${tokenString}`;
            }
        }
        const response = await this.request({
            path: `/api/v1/groups/group/{groupId}`.replace(`{${"groupId"}}`, encodeURIComponent(String(requestParameters.groupId))),
            method: 'GET',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => GroupDtoFromJSON(jsonValue));
    }

    /**
     */
    async getGroup(groupId: number, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<GroupDto> {
        const response = await this.getGroupRaw({ groupId: groupId }, initOverrides);
        return await response.value();
    }

    /**
     */
    async removeFromGroupRaw(requestParameters: RemoveFromGroupRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<GroupDto>> {
        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json';

        if (this.configuration && this.configuration.accessToken) {
            const token = this.configuration.accessToken;
            const tokenString = await token("BearerJWT", []);

            if (tokenString) {
                headerParameters["Authorization"] = `Bearer ${tokenString}`;
            }
        }
        const response = await this.request({
            path: `/api/v1/groups/remove-from-group`,
            method: 'DELETE',
            headers: headerParameters,
            query: queryParameters,
            body: AssignedGroupCommandDtoToJSON(requestParameters.assignedGroupCommandDto),
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => GroupDtoFromJSON(jsonValue));
    }

    /**
     */
    async removeFromGroup(assignedGroupCommandDto?: AssignedGroupCommandDto, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<GroupDto> {
        const response = await this.removeFromGroupRaw({ assignedGroupCommandDto: assignedGroupCommandDto }, initOverrides);
        return await response.value();
    }

    /**
     */
    async updateGroupRaw(requestParameters: UpdateGroupRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<GroupDto>> {
        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json';

        if (this.configuration && this.configuration.accessToken) {
            const token = this.configuration.accessToken;
            const tokenString = await token("BearerJWT", []);

            if (tokenString) {
                headerParameters["Authorization"] = `Bearer ${tokenString}`;
            }
        }
        const response = await this.request({
            path: `/api/v1/groups/group`,
            method: 'PUT',
            headers: headerParameters,
            query: queryParameters,
            body: UpdateGroupCommandDtoToJSON(requestParameters.updateGroupCommandDto),
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => GroupDtoFromJSON(jsonValue));
    }

    /**
     */
    async updateGroup(updateGroupCommandDto?: UpdateGroupCommandDto, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<GroupDto> {
        const response = await this.updateGroupRaw({ updateGroupCommandDto: updateGroupCommandDto }, initOverrides);
        return await response.value();
    }

}
